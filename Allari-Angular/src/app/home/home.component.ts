import { Component, OnInit, HostListener } from '@angular/core';
import { ICarousel } from '../carousel/carousel.interface';
import { BehaviorSubject, Observable } from 'rxjs';
import { CarouselService } from '../carousel/carousel.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor(private _carouselService: CarouselService) {}
  listImages: ICarousel[] = [];

  async ngOnInit() {
    const numberOfImages = Math.floor(Math.random() * 100) + 1;
    this.listImages = await this._carouselService.getImagesList(numberOfImages);
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
