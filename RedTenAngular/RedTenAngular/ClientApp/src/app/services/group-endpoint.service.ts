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
export class GroupEndpointService extends EndpointBase {

  get groupsEndpoint() { return this.configurations.baseUrl + '/api/Groups'; }


  constructor(private configurations: ConfigurationService, http: HttpClient, auth: AuthService)
  {
    super(http, auth);
  }

  getGroupEndpoint<T>(): Observable<T> {
    const endpointURL: string = this.groupsEndpoint;

    return this.http.get<T>(endpointURL, this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getGroupEndpoint());
      }));
  }

  getNewGroupEndpoint<T>(groupObj: T): Observable<T> {
    
    return this.http.post<T>(this.groupsEndpoint, JSON.stringify(groupObj), this.requestHeaders).pipe<T>(
      catchError(error => {
        return this.handleError(error, () => this.getNewGroupEndpoint(groupObj));
      }));
  }

}
