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
export class PlayerEndpointService extends EndpointBase {
  get playerEndpoint() { return this.configurations.baseUrl + '/api/Players'; }
  constructor(private configurations: ConfigurationService, http: HttpClient, auth: AuthService)
  {
    super(http, auth);
  }

  getPlayerEndpoint<T>(): Observable<T> {
    const endpointURL: string = this.playerEndpoint;
    return this.http.get<T>(endpointURL, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getPlayerEndpoint());
      }));
  }

  getNewPlayerEndpoint<T>(playerObj: T): Observable<T> {
    return this.http.post<T>(this.playerEndpoint, JSON.stringify(playerObj), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getNewPlayerEndpoint(playerObj));
      }));
  }
}
