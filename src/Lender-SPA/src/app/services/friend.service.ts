import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FriendModel } from '../models/friend.model';
import { UserLoginModel } from '../models/user-login.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class FriendService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  addFriend(formData: FormData) {
    return this.http.post(this.baseUrl + 'friend', formData);
  }

  getFriends() {
    return this.http.get<FriendModel[]>(this.baseUrl + 'friend');
  }

  getFriendById(id: number) {
    return this.http.get<FriendModel[]>(this.baseUrl + `friend/${id}`);
  }

  removeFriend(id: number) {
    return this.http.delete(this.baseUrl + `friend/${id}`);
  }

  updateFriend(formData: FormData) { 
    return this.http.put(this.baseUrl + `friend`, formData);
  }
}
