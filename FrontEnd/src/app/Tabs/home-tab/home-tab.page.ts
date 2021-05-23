import { Component, EventEmitter, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { LoginPage } from 'src/app/pages/login/login.page';
import { TareaInfoPage } from 'src/app/pages/tarea-info/tarea-info.page';
import { HttpProviderService } from 'src/app/Services/http-provider/http-provider.service';

@Component({
  selector: 'app-home-tab',
  templateUrl: 'home-tab.page.html',
  styleUrls: ['home-tab.page.scss']
})
export class HomeTabPage {

  @Input() user: { login: boolean, user: string, password: string };

  pages=[
    {
      title: "Información General",
      url: "/tarea-info",
      icon: "information-circle"
    },
    {
      title: "Información del Equipo",
      url: "/team",
      icon: "people-circle"
    },
    {
      title: "Repositorio en GitHub",
      url: "/repositorio",
      icon: "logo-github"
    }
  ]

  constructor(public modalController: ModalController) { }

  ngOnInit() {
    //this.http.login("admin", "password").subscribe((data) => {console.log(data)});
    if (this.user == undefined) {
      this.user = {
        user: "",
        password: "",
        login: false
      }
    }
    if (!this.user.login) {
      this.abrirLogin();
    }
  }


  async abrirModal() {
    const modal = await this.modalController.create({
      component: TareaInfoPage
    });
    return await modal.present();
  }

  async abrirLogin() {
    let myEvent = new EventEmitter();
    myEvent.subscribe(res => {
      this.user = res;
    });

    const modal = await this.modalController.create({
      component: LoginPage,
      componentProps: {
        user: this.user.user,
        password: this.user.password,
        login: this.user.login,
        loginEvent: myEvent,
      }
    });
    return await modal.present();
  }


}
