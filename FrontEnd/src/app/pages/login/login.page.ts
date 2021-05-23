import { invalid } from '@angular/compiler/src/render3/view/util';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { Observable } from 'rxjs';

import { HttpProviderService } from 'src/app/Services/http-provider/http-provider.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  @Input() user: string;
  @Input() password: string;
  email: string;
  @Input() login: boolean;
  @Output() loginEvent = new EventEmitter();
  
  agregar:boolean;

  userObject: { login: boolean, user: string, password: string }

  constructor(private modalController: ModalController, private http: HttpProviderService) { }

  ngOnInit() {
    this.agregar = false;
  }

  salir() {
    this.modalController.dismiss();
  }

  ingresar() {
    this.http.login(this.user, this.password).subscribe((data) => {
      if (data) {
        this.userObject = {
          login: data,
          user: this.user,
          password: this.password
        }
        this.login = this.userObject.login;
        this.emitAlert("Sesion Iniciada", "Se ha iniciado sesion satisfactoriamente");
        this.loginEvent.emit(this.userObject);
      } else {

        this.emitAlert("Datos invalidos", "El usuario y contraseña son invalidos.")
      }
    });
  }

  cerrarSesion(){
    this.http.token = null;
    this.userObject = {
      login: false,
      user: "",
      password: ""
    }
    this.login = this.userObject.login;
    this.password = "";
    this.emitAlert("Sesion Concluida", "Se ha cerrado sesion satisfactoriamente");
    this.loginEvent.emit(this.userObject);
  }

  add(){
    this.agregar = true;
    this.user = "";
    this.email = "";
    this.password = "";
  }

  addUser(){

    let user = {
      userName: this.user,
      email: this.email,
      password: this.password
    }
  
      this.http.postRequest("Login", user)
        .subscribe(
          (data) => {
            this.emitAlert("Add", 'Successfully Added');
            // If we are in the last page, reload to show the "Next Page" button.
          },
          (err) => {
            if (err.status == 409 || err.status == 400)
              this.emitAlert("Add User", "One or more fields in the provided information infringe a constraint on the Data Base. Failed to Add.");
            else
              this.emitAlert("Add User", "An unexpected error ocurred while adding the record.");
          }
        )
        .add(
          () => { this.agregar = false; }
        );
  }

  cancel(){
    this.user = this.userObject.user;
    this.email = "";
    this.password = "";
  }

  async emitAlert(header: string, message: string) {
    const alert = document.createElement('ion-alert');
    alert.header = header;
    alert.message = message;
    alert.buttons = ['OK'];
  
    document.body.appendChild(alert);
    await alert.present();
  
    const { role } = await alert.onDidDismiss();
    //console.log('onDidDismiss resolved with role', role);
  }

}
