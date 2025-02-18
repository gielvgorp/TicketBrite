/// <reference types="cypress" />

describe('Events overview page', () => {
    it('Ticket selector should add ticket when clicking plus button', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get("[data-test='ticket-selector-container'] [data-test='ticket-selector']:first-child").as("TicketSelector").find("[data-test='ticket-selector-selected-amount']").should("have.text", "0");

        cy.get('@TicketSelector').first().find("button").last().click();

        cy.get('@TicketSelector').first().find("[data-test='ticket-selector-selected-amount']").should("have.text", "1");
    });

    it('Ticket selector button should disable at 10 tickets', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get("[data-test='ticket-selector-container'] [data-test='ticket-selector']:first-child").as("TicketSelector").find("[data-test='ticket-selector-selected-amount']").should("have.text", "0");

        for (let i = 0; i < 10; i++) {
            cy.get('@TicketSelector').first().find("button").last().click();
        }

        cy.get('@TicketSelector').first().find("button").last().should("be.disabled");
        cy.get('@TicketSelector').first().find("[data-test='ticket-selector-selected-amount']").should("have.text", "10");
    });

    it('Login warning should pop up when user wants to add ticket to shoppingcart', () => {
        cy.visit('http://localhost:5173/event/f827d813-e04a-4e84-8d69-72baef15fcd4');

        cy.get(".sign-in-container a").should("have.text", "Inloggen / Registreren");

        cy.get("[data-test='ticket-selector']:first-child [data-test='select-ticket']").first().as("TicketSelector").find("button").last().click();

        cy.get("[data-test='event-info-side-bar'] button.btn-success").first().click();

        cy.get(".modal").should("have.class", "show");
    });
});
