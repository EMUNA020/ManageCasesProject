import { Component, Input } from '@angular/core';
interface Case {
  id: number;
  fileNumber: string;
  caseName: string;
  status: string;
  openingDate: Date;
  isMyFile: boolean;
}
@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrl: './list.component.css'
})

export class ListComponent {
  @Input() cases: Case[] = [];
}
