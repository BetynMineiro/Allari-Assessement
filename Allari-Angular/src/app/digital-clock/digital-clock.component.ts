import { Component, OnInit } from '@angular/core';
import { interval } from 'rxjs';

@Component({
  selector: 'app-digital-clock',
  templateUrl: './digital-clock.component.html',
  styleUrls: ['./digital-clock.component.scss'],
})
export class DigitalClockComponent implements OnInit {
  constructor() {}
  currentTime: Date = new Date();
  ngOnInit() {
    const clock = interval(1000);
    clock.subscribe(() => {
      this.currentTime = new Date();
    });
  }
}
