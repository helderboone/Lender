import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { NgxSpinnerService } from 'ngx-spinner';
import { ImageHelper } from 'src/app/helpers/image.helper';
import { FriendModel } from 'src/app/models/friend.model';
import { AlertifyService } from 'src/app/services/alertify.service';
import { FriendService } from 'src/app/services/friend.service';

@Component({
  selector: 'app-edit-friend',
  templateUrl: './edit-friend.component.html',
  styleUrls: ['./edit-friend.component.css']
})
export class EditFriendComponent implements OnInit {
  @ViewChild('fileUploadInput') fileUploadInput: ElementRef;
  @ViewChild('fakeFileUploadInput') fakeFileUploadInput: ElementRef;
  editForm: FormGroup;
  friendModel: FriendModel;
  submitted = false;

  public uploader: FileUploader = new FileUploader({});


  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private friendService: FriendService,
    private alertifyService: AlertifyService,
    private spinner: NgxSpinnerService,
    public imageHelper: ImageHelper) { }

  ngOnInit(): void {
    localStorage.removeItem('image');
    this.initializeForm();
    this.populateForm();
    this.setUploaderCallbacks();
  }

  setUploaderCallbacks() {
    this.uploader.onBeforeUploadItem = (item) => {
      item.withCredentials = false;
    }
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };
    this.uploader.onCompleteItem = (item: any, response: any, status: any, headers: any) => {
      console.log('FileUpload:uploaded:', item, status, response);
    };
  }

  initializeForm() {
    this.editForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', Validators.required],
      phone: ['', Validators.required],
      number: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      neighborhood: ['', Validators.required]
    });
  }

  async populateForm() {
    this.route.params.subscribe(data => {
      this.friendService.getFriendById(data["id"]).subscribe(data => {
        this.friendModel = JSON.parse(JSON.stringify(data));
        this.editForm.patchValue({
          name: this.friendModel.name,
          email: this.friendModel.email,
          phone: this.friendModel.phone,
          number: this.friendModel.number,
          street: this.friendModel.street,
          city: this.friendModel.city,
          neighborhood: this.friendModel.neighborhood,
          photoUrl: this.friendModel.photoUrl
        });
        if (this.friendModel.photoUrl) {
          var photoUrlArray = this.friendModel.photoUrl.split('/');
          this.imageHelper.dataURLtoFile(this.friendModel.photoUrl, photoUrlArray.pop()).then(file => {
            this.friendModel.File = file;
          });
        }
      });
    });
  }

  selectFile() {
    this.fileUploadInput.nativeElement.click();
  }

  public findInvalidControls() {
    const invalid = [];
    const controls = this.editForm.controls;
    for (const name in controls) {
      if (controls[name].invalid) {
        invalid.push(name);
      }
    }
    return invalid;
  }

  onSubmit(): void {
    this.submitted = true;
    if (this.editForm.valid) {
      let friendForm = this.editForm.value;
      friendForm.id = this.friendModel.id;
      const formData = this.populateFormData(friendForm);
      localStorage.setItem('image', friendForm.File);
      this.spinner.show();
      this.friendService.updateFriend(formData)
        .subscribe(data => {
          setTimeout(() => {
            this.spinner.hide();
            this.router.navigate(['amigos']);
            this.alertifyService.success("Your profile has been successfully updated!");
          }, 1000);
        },
          error => {
            console.log(error)
          });
    }
  }

  populateFormData(friendForm: any) {
    const formData = new FormData();
    friendForm.File = this.friendModel.File;
    for (var key in friendForm)
      formData.append(key, friendForm[key]);

    return formData;
  }

  handleFileSelect(evt) {
    const files = evt.target.files;
    const file = files[0];

    if (files && file) {
      this.friendModel.File = file;
      const reader = new FileReader();
      reader.onload = this._handleReaderLoaded.bind(this);
      reader.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readerEvt) {
    const binaryString = readerEvt.target.result;
    this.friendModel.photoUrl = 'data:image/jpg;base64,' + btoa(binaryString);
  }

}
