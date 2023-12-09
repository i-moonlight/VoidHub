import { ToastrService } from "ngx-toastr";

export class ToastrExtension {
    static handleErrors(toastr: ToastrService, errs: string[]) {
      errs.forEach(err => {
          toastr.error(err);
      });
    }
}
