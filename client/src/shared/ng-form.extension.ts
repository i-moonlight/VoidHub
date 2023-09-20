import { NgForm } from "@angular/forms";

export class NgFormExtension{

  static markAllAsTouched(form: NgForm) {
    let controls = form.controls;
    for(let i = 0; i < Object.keys(controls).length; i++) {
      let control = controls[Object.keys(controls)[i]];
      control.markAsTouched();
    }
  }

}
