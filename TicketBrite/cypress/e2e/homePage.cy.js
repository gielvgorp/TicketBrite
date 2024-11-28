describe('Home Page', () => {
  it('should display events in a grid on the homepage', () => {
    // Bezoek de homepagina
    cy.visit('http://localhost:5173/');

    cy.get('._eventsGrid_tfaik_51').should('exist');

    cy.get('._eventsGrid_tfaik_51 ._gridItem_zlykb_1').should('have.length.greaterThan', 0);

    cy.get('._eventsGrid_tfaik_51 ._gridItem_zlykb_1').each(($el) => {

      cy.wrap($el).find('._artist_zlykb_103').should('exist');
      cy.wrap($el).find('._category_zlykb_93').should('exist');
    });
  });

  // TODO: check if redirection to correct event
  it("Should redirect to event details page when click on event", () => {
    cy.visit('http://localhost:5173/');

    cy.get('._eventsGrid_tfaik_51', { timeout: 10000 }).should('exist');
    cy.get('._eventsGrid_tfaik_51 ._gridItem_zlykb_1').first().as('firstEvent').should('be.visible');

    cy.get('@firstEvent').find('._artist_zlykb_103').invoke('text').as('eventTitle');

    cy.get('@firstEvent').click();

    //cy.url().should('contain.value', '/event/');
  });
});
