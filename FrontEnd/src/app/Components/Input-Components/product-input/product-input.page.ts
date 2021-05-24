import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ModalController } from '@ionic/angular';
import ProductModels from 'src/app/models/product-models';

@Component({
  selector: 'app-product-input',
  templateUrl: './product-input.page.html',
  styleUrls: ['./product-input.page.scss'],
})
export class ProductInputPage implements OnInit {

  @Input() id: number;
  @Input() name: string;
  @Input() price: number;
  @Input() discontinued: boolean;
  @Input() edit: boolean;
  @Input() agregar: boolean;

  @Output() element = new EventEmitter();

  descontinuado: string;

  constructor(private modalController: ModalController) { }

  ngOnInit() {
    if (this.discontinued) {
      this.descontinuado = "true"
    } else {
      this.descontinuado = "false";
    }
  }

  salir() {
    this.modalController.dismiss();
  }

  toggleDiscontinued() {
    if (this.descontinuado == "true") {
      this.discontinued = true;
    } else {
      this.discontinued = false;
    }
  }

  // Emite un evento con la información del registro modificado.
  emitUpdateEvent() {
    let modifiedPorduct = {
      id: this.id,
      name: this.name,
      price: this.price,
      isDiscontinued: this.discontinued
    }

    this.element.emit(modifiedPorduct);
  }

  // Emite un evento con la información del nuevo registro.
  emitPostEvent() {
    let product: ProductModels.IProductPost = {
      name: this.name,
      price: this.price,
      isDiscontinued: this.discontinued
    }

    this.element.emit(product);
  }

}
