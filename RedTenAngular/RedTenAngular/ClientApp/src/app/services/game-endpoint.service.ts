import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { AuthService } from './auth.service';
import { EndpointBase } from './endpoint-base.service';
import { ConfigurationService } from './configuration.service';

@Injectable({
  providedIn: 'root'
})
export class GameEndpointService extends EndpointBase {

  get gamesEndpoint() { return this.configurations.baseUrl + '/api/Games'; }

  constructor(private configurations: ConfigurationService, http: HttpClient, auth: AuthService) {
    super(http, auth);
  }

  getGameEndpoint<T>(): Observable<T> {
    const endpointURL: string = this.gamesEndpoint;

    return this.http.get<T>(endpointURL, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getGameEndpoint());
      }));
  }

  getNewGameEndpoint<T>(gameObj: T): Observable<T> {
    return this.http.post<T>(this.gamesEndpoint, JSON.stringify(gameObj), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getNewGameEndpoint(gameObj));
      }));
  }

}
