import { Component, OnInit } from '@angular/core';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { FriendModel } from 'src/app/models/friend.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { FriendService } from 'src/app/services/friend.service';

@Component({
  selector: 'app-list-friend',
  templateUrl: './list-friend.component.html',
  styleUrls: ['./list-friend.component.css']
})
export class ListFriendComponent implements OnInit {

  friends: FriendModel[];

  constructor(private friendService: FriendService,
    private alertifyService: AlertifyService,
    public imageHelper: ImageHelper) {
      this.populateFriends();
  }

  ngOnInit(): void {
  }

  remove(id: number) {
    this.alertifyService.confirm('Voce tem certeza que quer deletar esse amigo?', () => {
      this.friendService.removeFriend(id).subscribe(() => {
        this.alertifyService.success('O amigo foi deletado');
        this.populateFriends();
      }, error => {
        if (error.error && error.error[0].Message)
          this.alertifyService.error(error.error[0].Message);
        else
          this.alertifyService.error('Falhou em deletar o amigo');
      });
    });
  }

  populateFriends(){
    this.friendService.getFriends().subscribe(data => {
      this.friends = data;
    });
  }

}
