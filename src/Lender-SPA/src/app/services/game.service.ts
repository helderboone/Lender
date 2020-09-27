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

  addGame(gameModel: GameModel) {
    return this.http.post(this.baseUrl + 'game', gameModel);
  }

  getGames() {
    return this.http.get<GameModel[]>(this.baseUrl + 'game');
  }

  getGameById(id: number) {
    return this.http.get<GameModel[]>(this.baseUrl + `Game/${id}`);
  }

  removeGame(id: number) {
    return this.http.delete(this.baseUrl + `Game/${id}`);
  }

  updateGame(gameModel: GameModel) {
    return this.http.put(this.baseUrl + `Game`, gameModel);
  }
}
