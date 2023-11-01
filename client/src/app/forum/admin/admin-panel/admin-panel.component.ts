import { Component, OnDestroy } from '@angular/core';
import { AdminService } from '../admin.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ReplaySubject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnDestroy  {

  private readonly destroy$ = new ReplaySubject<boolean>(1);

  constructor(
    private adminService: AdminService,
    private router: Router,
    private route: ActivatedRoute) {
      this.adminService.cancelClicked
      .pipe(takeUntil(this.destroy$))
      .subscribe(_ => this.close());
    }

  close() {
    console.log('close');

    this.router.navigate(['../', 'admin-panel'], { relativeTo: this.route });
  }

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }
}
