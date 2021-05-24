import { Component, EventEmitter, Input } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { LoginPage } from 'src/app/Components/Input-Components/login/login.page';
import { TareaInfoPage } from 'src/app/Components/Home-Components/tarea-info/tarea-info.page';
import { HttpProviderService } from 'src/app/Services/http-provider.service';

@Component({
  selector: 'app-home-tab',
  templateUrl: 'home-tab.page.html',
  styleUrls: ['home-tab.page.scss']
})
export class HomeTabPage {

  userObject: { logedOn: boolean, user: string }

  pages = [
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
    this.userObject = {
      user: "",
      logedOn: false
    }

    this.abrirLogin();
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
      this.userObject = res;
    });

    const modal = await this.modalController.create({
      component: LoginPage,
      componentProps: {
        user: this.userObject.user,
        logedOn: this.userObject.logedOn,
        loginEvent: myEvent,
      }
    });

    return await modal.present();
  }


}
