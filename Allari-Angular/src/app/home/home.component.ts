import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  constructor() {}
  list = [
    {
      url: 'https://picsum.photos/200/300',
      title: 'teste 1',
    },
    {
      url: 'https://picsum.photos/200/300',
      title: 'teste 2',
    },
  ];
  ngOnInit() {}
}
