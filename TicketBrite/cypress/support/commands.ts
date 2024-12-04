/// <reference types="cypress" />

Cypress.Commands.add('login', (email: string, password: string) => {

    cy.visit('/authenticatie');
    cy.get('#email-input').type(email); // Enter the email
    cy.get('#password-input').type(password); // Enter the password
    cy.get('button[type="submit"]').click(); // Click the login button

    cy.get(".sign-in-container a").should("contain.text", "Welkom, ");
});

Cypress.Commands.add('register', (email: string, fullname: string, password: string) => {

    cy.visit('/authenticatie/register');
    cy.get('#email-input').type(email); // Enter the email
    cy.get('#full-name-input').type(fullname); // Enter the email
    cy.get('#password-input').type(password); // Enter the password
    cy.get('button[type="submit"]').click(); // Click the login button
});

Cypress.Commands.add('navigateToOrganizationPanel', () => {
    cy.get(".sign-in-container a").click();
    cy.get("#profile-organization").should("exist");
    cy.get("#profile-organization").click();
    cy.get("#submenu1").click();
});

Cypress.Commands.add('navigateToAdminPanel', () => {
    cy.get(".sign-in-container a").click();
    cy.get("#profile-admin").should("exist");
    cy.get("#profile-admin").click();
    cy.get("#submenu2").click();
});

