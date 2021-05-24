import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { EmployeeModels } from 'src/app/Models/employee-models';
import { EmployeeInputPage } from '../../Input-Components/employee-input/employee-input.page';

@Component({
  selector: 'app-employee-info',
  templateUrl: './employee-info.component.html',
  styleUrls: ['./employee-info.component.scss'],
})
export class EmployeeInfoComponent implements OnInit {
  @Input() employee: EmployeeModels.IEmployee;

  @Output() deleteEvent = new EventEmitter();
  @Output() updateEvent = new EventEmitter();

  constructor(public modalController: ModalController) { }

  ngOnInit() { }

  delete(id: number) {
    this.deleteEvent.emit(id);
  }

  update(id: number) {
    this.abrirModal(true, false);
  }

  async abrirModal(editable: boolean, agregable: boolean) {
    let myEvent = new EventEmitter();
    myEvent.subscribe(res => {
      this.employee = res;
      this.updateEvent.emit(this.employee);

      modal.dismiss();
    });

    const modal = await this.modalController.create({
      component: EmployeeInputPage,
      componentProps: {
        id: this.employee.id,
        name: this.employee.name,
        familyName: this.employee.familyName,
        homeAddress: this.employee.homeAddress,
        edit: editable,
        agregar: agregable,
        element: myEvent
      }
    });

    return await modal.present();
  }

}
