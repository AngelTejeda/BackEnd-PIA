import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalController } from '@ionic/angular';

import { HttpProviderService } from 'src/app/Services/http-provider.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  @Output() loginEvent = new EventEmitter();
  @Input() user: string;
  @Input() logedOn: boolean;
  
  password: string;
  email: string;

  signUpView: boolean;
  loading: boolean;

  //userObject: { login: boolean, user: string }

  constructor(private modalController: ModalController, private http: HttpProviderService) { }

  ngOnInit() {
    this.signUpView = false;
  }

  // Cierra el modal y regresa a HomeTab
  closeModal() {
    this.modalController.dismiss();
  }

  // Función para cambiar a la vista de registro.
  changeToSingUpView() {
    this.signUpView = true;
    this.clearUserFields();
  }

   // Función para regresar a la vista de inicio de sesión.
  changeToLoginView() {
    this.signUpView = false;
    this.clearUserFields();
  }

  // Manda una petición GET al controlador Login para iniciar sesión
  login() {
    this.loading = true;

    // Petición GET devuelve true si el inicio de sesión fue correcto.
    this.http.login(this.user, this.password)
      // Esperamos respuesta
      .subscribe(
        (success) => {
          // Respuesta satisfactoria
          if(success) {
            this.logedOn = true;

            // Contenido del evento emitido a HomeTab.
            let userObject = {
              logedOn: true,
              user: this.user,
            }
  
            this.emitAlert("Sesion Iniciada", "Se ha iniciado sesion satisfactoriamente");
            this.loginEvent.emit(userObject);
          }

          // Error
          else {
            this.emitAlert("Datos invalidos", "El usuario o la contraseña son invalidos.")
          }

          this.loading = false;
        }
      );
  }

  // Borra el token obtenido.
  cerrarSesion() {
    this.clearUserFields();
    this.http.token = null;
    this.logedOn = false

    this.emitAlert("Sesion Concluida", "Se ha cerrado sesion satisfactoriamente");
    this.loginEvent.emit({user: this.user, logedOn: false});
  }

  // Función para registrar un nuevo usuario
  addUser() {
    // Email Regex
    var patt = new RegExp('^[A-Z0-9a-z\\._%+-]+@([A-Za-z0-9-]+\\.)+[A-Za-z]{2,4}$');
    var isMailValid = patt.test(this.email);

    // Si el correo válido.
    if(isMailValid){
      let user = {
        userName: this.user,
        email: this.email,
        password: this.password
      }

      // Petición POST
      this.http.postRequest("Login", user)
        // Esperamos Respuesta
        .subscribe(
          // Respuesta válida
          () => {
            this.emitAlert("Add", 'Successfully Added');
          },
          
          // Error
          (err) => {
            if (err.status == 409 || err.status == 400)
              this.emitAlert("Add User", "One or more fields in the provided information infringe a constraint on the Data Base. Failed to Add.");
            else
              this.emitAlert("Add User", "An unexpected error ocurred while adding the record.");
          }
        )
        .add(
          () => { this.signUpView = false; }
        );
      }
      else {
        this.clearUserFields();
        this.emitAlert("Add User","One or more fields in the provided information infringe a constraint on the Data Base. Failed to Add.");
      }
  }

  private clearUserFields() {
    this.user = "";
    this.password = "";
    this.email = "";
  }

  // Función para mostrar alertas al usuario.
  private async emitAlert(header: string, message: string) {
    const alert = document.createElement('ion-alert');
    alert.header = header;
    alert.message = message;
    alert.buttons = ['OK'];

    document.body.appendChild(alert);
    await alert.present();

    const { role } = await alert.onDidDismiss();
  }
}
