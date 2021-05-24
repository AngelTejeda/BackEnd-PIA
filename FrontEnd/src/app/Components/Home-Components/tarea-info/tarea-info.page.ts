import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-tarea-info',
  templateUrl: './tarea-info.page.html',
  styleUrls: ['./tarea-info.page.scss'],
})
export class TareaInfoPage implements OnInit {

  constructor(private modalController: ModalController) { }

  implementaciones=[
    "CRUD: Create",
    "CRUD: Read",
    "CRUD: Update",
    "CRUD: Delete",
    "Token Bearer JWT",
    "CORS Configurado",
    "Base de Datos SQL",
    "Frontend"
  ]

  ngOnInit() {
  }

  salir(){
    this.modalController.dismiss();
  }

}
