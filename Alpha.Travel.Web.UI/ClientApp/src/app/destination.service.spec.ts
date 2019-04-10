import { TestBed } from '@angular/core/testing';

import { DestinationService } from './destination.service';

describe('DestinationService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: DestinationService = TestBed.get(DestinationService);
    expect(service).toBeTruthy();
  });
});
