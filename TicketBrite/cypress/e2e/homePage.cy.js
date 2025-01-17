/// <reference types="cypress" />

describe('Home Page', () => {
  it('should display events in a grid on the homepage', () => {
    // Bezoek de homepagina
    cy.visit('http://localhost:5173/');

    cy.get("[data-test='event-grid']").should('exist');

    cy.get("[data-test='event-grid'] [data-test='grid-item']").should('have.length.greaterThan', 0);

    cy.get("[data-test='event-grid'] [data-test='grid-item']").each(($el) => {

      cy.wrap($el).find("[data-test='grid-item-artist']").should('exist');
      cy.wrap($el).find("[data-test='grid-item-category']").should('exist');
    });
  });

  // TODO: check if redirection to correct event
  it("Should redirect to event details page when click on event", () => {
    cy.visit('http://localhost:5173/');

    cy.get("[data-test='event-grid']", { timeout: 10000 }).should('exist');
    cy.get("[data-test='event-grid'] [data-test='grid-item']").first().as('firstEvent').should('be.visible');

    cy.get('@firstEvent').find("[data-test='grid-item-artist']").invoke('text').as('eventTitle');

    cy.get('@firstEvent').click();
  });
});
