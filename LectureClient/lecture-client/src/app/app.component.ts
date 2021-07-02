import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { environment } from '../environments/environment';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'lecture-client';
  inputMessage: string;
  notificationMessage: string;
  lectureUrl = `${environment.api}/api/lecture`;
  lectureBetaUrl = `${environment.api}/api/lectureBeta`;
  connection: HubConnection;

  constructor(private http: HttpClient) {}

  ngOnInit(){
    this.connection = new HubConnectionBuilder().withUrl(`${environment.api}/lecture`).build();
    this.connection.start().catch((error) => console.log(`SignalR connection: ${error}`));

    this.connection.on('GetNotification', (value: string) => {
      this.notificationMessage = value;
    });
  }

  public ngOnDestroy() {
    this.connection.stop();
}

  buttonLectureOnClick(){
    this.http.post(
      this.lectureUrl, 
      JSON.stringify(this.inputMessage), 
      { headers: new HttpHeaders({'Content-Type':'application/json; charset=utf-8'}),  observe: 'response' }).subscribe();
  }

  buttonLectureBetaOnClick(){
    this.http.post(
      this.lectureBetaUrl, 
      JSON.stringify(this.inputMessage), 
      { headers: new HttpHeaders({'Content-Type':'application/json; charset=utf-8'}),  observe: 'response' }).subscribe();
  }
  }
}
