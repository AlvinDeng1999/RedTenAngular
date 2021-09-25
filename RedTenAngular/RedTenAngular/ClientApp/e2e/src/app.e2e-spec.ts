// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

import { AppPage } from './app.po';

describe('RedTenAngular App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display application title: RedTenAngular', async () => {
    await page.navigateTo();
    expect(await page.getAppTitle()).toEqual('RedTenAngular');
  });
});
