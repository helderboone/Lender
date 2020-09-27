import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FriendModel } from 'src/app/models/friend.model';
import { FriendService } from 'src/app/services/friend.service';

@Component({
  selector: 'app-detail-friend',
  templateUrl: './detail-friend.component.html',
  styleUrls: ['./detail-friend.component.css']
})
export class DetailFriendComponent implements OnInit {
  
  public friendModel: FriendModel;

  public defaultImageUrl:string = '../../../../assets/default-profile-icon.jpg';

  constructor(private route: ActivatedRoute, private friendService: FriendService) {
    this.route.params.subscribe(data => {
      friendService.getFriendById(data["id"]).subscribe(data => {
        this.friendModel = JSON.parse(JSON.stringify(data)); 
      });
    });
  }

  ngOnInit(): void {
  }
}
