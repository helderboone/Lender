import { Injectable } from '@angular/core'; 
 
import { throwError } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})

export abstract class BaseService {
  constructor() { }

  public Token: string = "";

  protected baseUrl: string = environment.apiUrl;

  protected async extractData(response: Response) {
      let body = await response;
      return body || {};
  }

  public getUser() {
      return JSON.parse(localStorage.getItem('user'));
  }

  protected async serviceError(error: Response | any) {
      let errMsg: string;
      if (error instanceof Response) {
          const body = await error.json() || '';
          const err = body.error || JSON.stringify(body);
          errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
      } else {
          errMsg = error.message ? error.message : error.toString();
      }

      return throwError(error);
  }
}