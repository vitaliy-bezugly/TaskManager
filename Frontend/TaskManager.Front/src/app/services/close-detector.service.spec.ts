import { TestBed } from '@angular/core/testing';

import { CloseDetectorService } from './close-detector.service';

describe('CloseDetectorService', () => {
  let service: CloseDetectorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CloseDetectorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
