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
export class RoundEndpointService extends EndpointBase {
  get roundEndpoint() { return this.configurations.baseUrl + '/api/Rounds'; }

  constructor(private configurations: ConfigurationService, http: HttpClient, auth: AuthService) {
    super(http, auth);
  }

  getRoundEndpoint<T>(): Observable<T> {
    const endpointURL: string = this.roundEndpoint;
    return this.http.get<T>(endpointURL, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getRoundEndpoint());
      }));
  }

  getNewRoundEndpoint<T>(roundObj: T): Observable<T> {
    return this.http.post<T>(this.roundEndpoint, JSON.stringify(roundObj), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getNewRoundEndpoint(roundObj));
      }));
  }

}
