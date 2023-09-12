import { Component, OnInit, HostListener } from '@angular/core';
import { ICarousel } from '../carousel/carousel.interface';
import { BehaviorSubject, Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor() {}
  list: ICarousel[] = [];
  foods: any[] = [
    { value: 'steak-0', viewValue: 'Steak' },
    { value: 'pizza-1', viewValue: 'Pizza' },
    { value: 'tacos-2', viewValue: 'Tacos' },
  ];
  ngOnInit() {
    for (let index = 0; index < 10; index++) {
      this.list.push({
        url: this.generateRandomImage(),
        title: `Image ${index + 1}`,
      });
    }
  }
  generateRandomImage(): string {
    const id = Math.floor(Math.random() * 1000);
    return `https://picsum.photos/600/600?random=${id}`;
  }
  private mouseCoordinatesSubject = new BehaviorSubject<{
    x: number;
    y: number;
  }>({ x: 0, y: 0 });
  mouseCoordinates$: Observable<{ x: number; y: number }> =
    this.mouseCoordinatesSubject.asObservable();
  @HostListener('mousemove', ['$event'])
  onMouseMove(event: MouseEvent): void {
    const coordinates = { x: event.clientX, y: event.clientY };
    this.mouseCoordinatesSubject.next(coordinates);
  }
}
