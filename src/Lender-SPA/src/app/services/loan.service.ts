import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoanModel } from '../models/loan.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class LoanService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }

  addLoan(loanModel: LoanModel) {
    return this.http.post(this.baseUrl + 'Loan', loanModel);
  }

  getLoans() {
    return this.http.get<LoanModel[]>(this.baseUrl + 'Loan');
  }

  getLoanById(id: number) {
    return this.http.get<LoanModel[]>(this.baseUrl + `Loan/${id}`);
  }

  endLoan(loanModel: LoanModel) {
    return this.http.put(this.baseUrl + `Loan`, loanModel);
  }

}
