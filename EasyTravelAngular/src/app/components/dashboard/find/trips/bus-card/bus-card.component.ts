import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BusService, BusMonitorService } from 'src/app/services';
import { Time } from '@angular/common';
import { BusTrip, Request, BaseTrip, TripType, BusMonitor } from 'src/app/models';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-bus-card',
  templateUrl: './bus-card.component.html',
  styleUrls: ['./bus-card.component.css']
})
export class BusCardComponent implements OnInit {

  constructor(private busService: BusService, private monitoringService: BusMonitorService, private snackBar: MatSnackBar) { }

  @Output() submitButton = new EventEmitter();
  @Input() from: string;
  @Input() to: string;
  @Input() date: string;
  @Input() time: Time;
  buses: BusTrip[];
  isLoading = true;
  isOpened = false;

  ngOnInit() {
  }

  busPanelClick() {
    if (this.buses != null) return;
    this.isOpened = true;
    var request = new Request();
    request.from = this.from;
    request.to = this.to;
    request.date = this.date;
    request.time = this.time;
    this.busService.getAllBuses(request).subscribe(buses => {
      this.isLoading = false;
      this.buses = buses;
    })
  }

  refresh(from: string, to: string, date: string, time: Time) {
    this.from = from;
    this.to = to;
    this.date = date;
    if (!this.isOpened) return;
    this.isLoading = true;
    this.buses.length = 0;
    var request = new Request();
    request.from = this.from;
    request.to = this.to;
    request.date = this.date;
    this.busService.getAllBuses(request).subscribe(buses => {
      this.buses = buses;
      this.isLoading = false;
    })
  }

  submit(bus: BusTrip) {
    var trip = new BaseTrip();
    trip.type = TripType.Bus;
    trip.bus = bus;
    this.submitButton.emit(trip);
    this.buses.length = 0;
  }

  createMonitoring() {
    var monitoring = new BusMonitor();
    monitoring.from = this.from;
    monitoring.to = this.to;
    monitoring.departureDate = this.date;
    this.monitoringService.create(monitoring).subscribe(result => {
      this.snackBar.open("Моніторинг створено", "Закрити", {
        duration: 3000
      });
    },
      error => {
        this.snackBar.open(error, "Закрити", {
          duration: 3000
        });
      });
  }
}
