import { Component, EventEmitter, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { IResponse } from 'src/app/Models/api-response-model';
import { ProductModels } from 'src/app/models/product-models';
import { HelpInfoPage } from 'src/app/pages/help-info/help-info.page';
import { ProductInputPage } from 'src/app/Components/Input-Components/product-input/product-input.page';
import { HttpProviderService } from 'src/app/Services/http-provider/http-provider.service';

@Component({
  selector: 'app-product-tab',
  templateUrl: './product-tab.page.html',
  styleUrls: ['./product-tab.page.scss'],
})
export class ProductTabPage implements OnInit {

  name = "Product";
  iconAdd = "add"

  // Valores de Respuesta de la API
  products: ProductModels.IProduct[] = []
  nextPage?: number = null;
  currentPage?: number = null;
  previousPage?: number = null;

  // Variables de navegación
  loaded: boolean = false;
  error: boolean = false;
  loading: boolean = false;
  addingElement: boolean = false;

  constructor(private http: HttpProviderService, private modalController: ModalController) {}

  ngOnInit() {
    this.getPage(1);
  }

  // Manda una petición para obtener la página actual.
  reloadCurrentPage() {
    if (this.currentPage == null)
      this.getPage(1)
    else
      this.getPage(this.currentPage);
  }

  // Manda una petición para obtener la página anterior.
  getPreviousPage() {
    this.getPage(this.previousPage);
  }

  // Manda una petición para obtener la página siguiente.
  getNextPage() {
    this.getPage(this.nextPage);
  }

  // Hace una petición para obtener una página de la tabla de la Base de Datos.
  private getPage(page: number) {
    this.loading = true;

    // Petición
    this.http.getRequest<IResponse<ProductModels.IProduct>>(this.name, `pages/${page}`)
      // Esperamos respuesta.
      .subscribe(
        // Respuesta satisfactoria.
        (data) => {
          // Cargamos la información de la respuesta.
          this.nextPage = data.nextPage;
          this.currentPage = data.currentPage;
          this.previousPage = data.previousPage;
          this.products = data.responseList;

          // Verificamos que la lista de respuesta no esté vacía.
          this.loaded = this.products.length > 0 ? true : false;

          // La API respondió satisfactoriamente.
          this.error = false;
        },

        // Error
        (err) => {
          if(err.status == 401)
            this.emitAlert("Lista de Registros", "No está autorizado. Inicie sesión para obtener su token de acceso.");
          
          // Se resetean los valores.
          this.nextPage = null;
          this.currentPage = null;
          this.previousPage = null;

          this.error = true;
          this.loaded = false;
        }
      )
      .add(() => {
        this.loading = false;
      });
  }

  // Agregar un registro.
  addElement(employee: ProductModels.IProductPost) {
    this.addingElement = true;

    // Petición POST
    this.http.postRequest(this.name, employee)
      // Esperamos respuesta
      .subscribe(
        // Respuesta satisfactoria.
        (data: any) => {
          // Si nos encontramos en la última página, recargamos para mostrar el botón de "siguiente".
          if (!this.nextPage)
            this.reloadCurrentPage();
          
          this.emitAlert("Agregar", `Se agregó el registro con id ${data.id}.`);
        },

        // Error
        (err) => {
          if (err.status == 409 || err.status == 400)
            this.emitAlert("Agregar", "Uno o más campos no cumplen las restricciones de la tabla. No se pudo modificar el registro.");
          else if(err.status == 401)
            this.emitAlert("Agregar", "No está autorizado. Inicie sesión para obtener su token de acceso.");
          else
            this.emitAlert("Agregar", "Ha ocurrido un error inesperado.")
        }
      )
      .add(
        () => { this.addingElement = false; }
      );
  }

  // Modificar un registro
  updateElement($event: ProductModels.IProduct) {
    let employee: ProductModels.IProduct = $event;

    // Petición PUT
    this.http.putRequest(this.name, employee.id.toString(), employee)
      // Esperamos respuesta.
      .subscribe(
        // Respuesta satisfactoria.
        () => {
          this.emitAlert("Actualizar", "Registro modificado.");
        },

        // Error
        (err) => {
          if (err.status == 409 || err.status == 400)
            this.emitAlert("Actualizar", "Uno o más campos no cumplen las restricciones de la tabla. No se pudo modificar el registro.");
          else if(err.status == 401)
            this.emitAlert("Actualizar", "No está autorizado. Inicie sesión para obtener su token de acceso.");
          else
            this.emitAlert("Actualizar", "Ha ocurrido un error inesperado.")
        }
      )
      // Recargar la página para mostrar los cambios.
      .add(() => {
        this.reloadCurrentPage();
      });
  }

  // Eliminar registro
  deleteElement($event: number) {
    let id = $event;

    // Petición DELETE
    this.http.deleteRequest(this.name, `${id}`)
      // Esperamos respuesta
      .subscribe(
        // Respuesta satisfactioria
        () => {
          // Si eliminamos el último registro de una página.
          if (this.products.length == 1) {
            // Ir a la página anterior (si existe).
            if (this.previousPage)
              this.getPreviousPage();

            // Si no existe, ir a la página siguiente.
            else if (this.nextPage)
              this.getNextPage();

            // Si tampoco existe, mostrar el mensaje de que no hay más elementos.
            else {
              this.nextPage = null;
              this.currentPage = null;
              this.previousPage = null;

              this.loaded = false;
            }
          }

          // Si no es el último elemento, recargar para mostrar cambios.
          else
            this.reloadCurrentPage();
        },

        // Error
        (err) => {
          if (err.status == 409 || err.status == 400)
            this.emitAlert("Eliminar", "No se puede eliminar el registro porque es llave foránea.")
          else if(err.status == 401)
            this.emitAlert("Eliminar", "No está autorizado. Inicie sesión para obtener su token de acceso.");
          else
            this.emitAlert("Eliminar", "Ha ocurrido un error inesperado.")
        }
      );
  }

  // Modal para mostrar, editar o agregar un registro.
  async abrirModal(editable: boolean, agregable: boolean) {
    let myEvent = new EventEmitter();
    myEvent.subscribe(res => {
      modal.dismiss();
      this.addElement(res);
    });

    const modal = await this.modalController.create({
      component: ProductInputPage,
      componentProps: {
        id: "",
        company: "",
        contactFullName: "",
        edit: editable,
        agregar: agregable,
        element: myEvent
      }
    });
    return await modal.present();
  }

  private async emitAlert(header: string, message: string) {
    const alert = document.createElement('ion-alert');
    alert.header = header + " Product";
    alert.message = message;
    alert.buttons = ['OK'];

    document.body.appendChild(alert);
    await alert.present();

    const { role } = await alert.onDidDismiss();
  }

  async abrirAyuda() {
    const modal = await this.modalController.create({
      component: HelpInfoPage,
    });
    return await modal.present();
  }

}
