/// <reference types="cypress" />

describe('Test admin role', () => {
    it('Unverified events should be visable in admin screen', () => {
        cy.login(Cypress.env('testAdmin'), Cypress.env('testPassword'));

        cy.navigateToAdminPanel();

    });
});