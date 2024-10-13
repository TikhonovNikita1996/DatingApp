import { inject, Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  bussyRequestCount = 0;
  private spinnerService=inject(NgxSpinnerService);

  busy(){
    this.bussyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'pacman',
      bdColor: 'rgba(255,255,255,0)',
      color: '#333333'
    })
  }

  idle(){
    this.bussyRequestCount--;
    if(this.bussyRequestCount <= 0) {
      this.bussyRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
