import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ICarousel } from './carousel.interface';

@Injectable({
  providedIn: 'root',
})
export class CarouselService {
  constructor(private _httpClient: HttpClient) {}

  async getImagesList(numberOfImages: number) {
    return lastValueFrom(
      this._httpClient.get<ICarousel[]>(
        `http://localhost:5141/api/AllariAssessement/imageinfo/size/${numberOfImages}`
      )
    );
  }
}
