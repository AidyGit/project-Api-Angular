import { ChangeDetectorRef, Component, OnInit, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DataViewModule } from 'primeng/dataview';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ButtonModule } from 'primeng/button';
import { getDonation } from '../../../models/manage/donationModels/getDonationModel';
import { CommonModule } from '@angular/common';
import { DonationService } from '../../../service/manage/donation/donation.service';
import { SharedModule } from 'primeng/api';

@Component({
  standalone: true,
  selector: 'app-donation',
imports: [
    CommonModule, 
    DataViewModule, 
    SelectButtonModule, 
    ButtonModule, 
    FormsModule, 
    SharedModule // <--- זה בדרך כלל מה שחסר כדי שה-Templates יעבדו
  ],  templateUrl: './donation.html'
})
export class Donation {

  private donationService = inject(DonationService);
  donations = signal<getDonation[]>([]);
  private cdr = inject(ChangeDetectorRef);
  layout: 'list' | 'grid' = 'grid'; // וודא שזה תואם לערכים ב-options

  // עדכון האופציות לפורמט ש-SelectButton אוהב
  options = [
    { label: 'רשימה', value: 'list' },
    { label: 'רשת', value: 'grid' }
  ];
  ngOnInit() {
    this.donationService.getAll().subscribe({
      next: (data) => {
        console.log('נתונים שהתקבלו מהשרת:', data); // בדיקה מה הגיע
        this.donations.set(data);
        this.cdr.detectChanges(); // הכרחת עדכון מסך
        console.log('אורך המערך:', this.donations.length);
      },
      error: (err) => console.error('שגיאה:', err)
    });
  }
}
