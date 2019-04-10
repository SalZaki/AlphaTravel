import { Component, OnInit } from '@angular/core';
import { DestinationService } from '../destination.service'

@Component({
  selector: 'app-destinations',
  templateUrl: './destinations.component.html',
  styleUrls: ['./destinations.component.css']
})

export class DestinationsComponent implements OnInit {

  constructor(private service: DestinationService) { }

  ngOnInit() {
    this.service.getAll().subscribe((data) => {
      console.log('Result - ', data);
    })
  }
}
