import { Component, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { MemberPage } from '../member/member.page';

@Component({
  selector: 'app-team',
  templateUrl: './team.page.html',
  styleUrls: ['./team.page.scss'],
})
export class TeamPage implements OnInit {

  constructor(public modalController: ModalController) { }

  ngOnInit() {
  }

  equipo=[
    {
      fullname:"José Santos Flores Silva",
      name: "José Flores",
      matricula: 1851125,
      imagen: "JoséFlores.png"
    },
    {
      fullname:"Edson Yael García Silva",
      name: "Edson García",
      matricula: 1863860,
      imagen: "EdsonGarcía.png"
    },
    {
      fullname:"Sofía Alejandra Gaytán Díaz",
      name: "Sofía Gaytán",
      matricula: 1845533,
      imagen: "SofíaGaytán.png"
    },
    {
      fullname:"Ángel Tejeda Tiscareño",
      name: "Ángel Tejeda",
      matricula: 1851388,
      imagen: "ÁngelTejeda.png"
    }
  ]

  async abrirModal(integrante: {fullname: string, name: string, matricula: number}) {

    const modal = await this.modalController.create({
      component: MemberPage,
      componentProps:{
        integrante: integrante
      }
    });

    return await modal.present();
  }

}
