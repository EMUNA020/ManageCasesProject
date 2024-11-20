import { Component } from '@angular/core';
import { ApiCallService } from '../../core/services/api-call.service';
interface Case {
  id: number;
  fileNumber: string;
  caseName: string;
  status: string;
  openingDate: Date;
  isMyFile: boolean; 
}

@Component({
  selector: 'app-cases',
  templateUrl: './cases.component.html',
  styleUrl: './cases.component.css'
})
export class CasesComponent {
 message :any;

  constructor(private apiService: ApiCallService) { }

  ngOnInit(): void {
    this.apiService.getList('Case/GetAllCases').subscribe((data) => {
      this.message = data;
    });
    this.apiService.getList('WeatherForecast/GetWeatherForecast').subscribe((data) => {
      this.message = data;
    });
  }
  cases: Case[] = [
    {
      id: 1,
      fileNumber: 'FN001',
      caseName: 'Case A',
      status: 'Active',
      openingDate: new Date('2024-01-15'),
      isMyFile: true// real application will use login user info,
    },
    {
      id: 2,
      fileNumber: 'FN005',
      caseName: 'Case B',
      status: 'Closed',
      openingDate: new Date('2024-02-20'),
      isMyFile: false,
    },
    {
      id: 3,
      fileNumber: 'FN003',
      caseName: 'Case C',
      status: 'Active',
      openingDate: new Date('2024-03-10'),
      isMyFile: true,
    },
    {
      id: 4,
      fileNumber: 'FN004',
      caseName: 'Case D',
      status: 'Active',
      openingDate: new Date('2024-01-15'),
      isMyFile: true// real application will use login user info,
    },
    {
      id: 5,
      fileNumber: 'FN006',
      caseName: 'Case E',
      status: 'Closed',
      openingDate: new Date('2024-02-20'),
      isMyFile: false,
    },
    {
      id: 6,
      fileNumber: 'FN000',
      caseName: 'Case F',
      status: 'Active',
      openingDate: new Date('2024-03-10'),
      isMyFile: true,
    },
    // Add more hardcoded cases as needed
  ];

  filteredCases: Case[] = [...this.cases];

  applyFilter(filter: { search: string; status: string; myFiles: boolean }) {
    this.filteredCases = this.cases.filter((c) => {
      const matchesSearch =
        !filter.search ||
        c.caseName.toLowerCase().includes(filter.search.toLowerCase());
      const matchesStatus =
        filter.status === 'Everything' || c.status === filter.status;
      const matchesMyFiles = !filter.myFiles || c.isMyFile;

      return matchesSearch && matchesStatus && matchesMyFiles;
    });
  }

  applySort(sortField: 'openingDate' | 'fileNumber') {
    this.filteredCases.sort((a, b) => {
      if (sortField === 'openingDate') {
        return a.openingDate.getTime() - b.openingDate.getTime();
      } else if (sortField === 'fileNumber') {
        return a.fileNumber.localeCompare(b.fileNumber);
      }
      return 0;
    });
  }
}
