import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GameModel } from '../models/game.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class GameService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  addGame(formData: FormData) {
    return this.http.post(this.baseUrl + 'game', formData);
  }

  getGames() {
    return this.http.get<GameModel[]>(this.baseUrl + 'game');
  }

  getNotBorrowedGames() {
    return this.http.get<GameModel[]>(this.baseUrl + 'game/not-borrowed');
  }

  getGameById(id: number) {
    return this.http.get<GameModel[]>(this.baseUrl + `Game/${id}`);
  }

  removeGame(id: number) {
    return this.http.delete(this.baseUrl + `Game/${id}`);
  }

  updateGame(formData: FormData) {
    return this.http.put(this.baseUrl + `Game`, formData);
  }
}
