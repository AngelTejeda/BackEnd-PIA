import { Component, Input, OnInit } from '@angular/core';
import { ModalController } from '@ionic/angular';

@Component({
  selector: 'app-member',
  templateUrl: './member.page.html',
  styleUrls: ['./member.page.scss'],
})
export class MemberPage implements OnInit {
  @Input() integrante: {fullname: string, name:string, matricula:number, imagen: string};

  imagen: string;

  constructor(private modalController: ModalController) { }

  ngOnInit() {

    this.imagen = "/assets/integrantes/" + this.integrante.imagen;

  }

  salir(){
    this.modalController.dismiss();
  }

  dormir(){
    let nombre = this.integrante.name.split(" ");
    let cambio = "/assets/integrantes/"+nombre[0]+nombre[1]+"2.png"
    if(this.imagen == cambio ){
      this.imagen = "/assets/integrantes/" + this.integrante.imagen;
    }else{
      this.imagen = cambio;
    }
  }

}
