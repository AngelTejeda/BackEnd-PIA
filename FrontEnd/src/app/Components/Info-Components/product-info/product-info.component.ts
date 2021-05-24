import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { ProductModels } from 'src/app/models/product-models';
import { ProductInputPage } from '../../Input-Components/product-input/product-input.page';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.scss'],
})
export class ProductInfoComponent implements OnInit {

  @Input() product: ProductModels.IProduct;

  @Output() deleteEvent = new EventEmitter();
  @Output() updateEvent = new EventEmitter();

  constructor(public modalController: ModalController) { }

  ngOnInit() { }

  // Emite un evento al padre para que haga una petición DELETE.
  delete(id: string) {
    this.deleteEvent.emit(id);
  }

  // Abre el modal en modo vista.
  show() {
    this.abrirModal(false, false);
  }

  // Abre el modal en modo edición
  update() {
    this.abrirModal(true, false);
  }

  // Abre el modal según los parámetros indicados
  async abrirModal(editable: boolean, agregable: boolean) {
    let myEvent = new EventEmitter();
    myEvent.subscribe(res => {
      this.product = res;
      this.updateEvent.emit(this.product);

      modal.dismiss();
    });

    const modal = await this.modalController.create({
      component: ProductInputPage,
      componentProps: {
        id: this.product.id,
        name: this.product.name,
        price: this.product.price,
        discontinued: this.product.isDiscontinued,
        edit: editable,
        agregar: agregable,
        element: myEvent
      }
    });

    return await modal.present();
  }

}
