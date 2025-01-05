/// <reference types="cypress" />
describe("Test shopping cart", () => {
    it("Should add ticket to shopping cart", () => {
        cy.login(Cypress.env('testUser'), Cypress.env("testPassword"));

        cy.get("[data-test='nav-shopping-cart']").click();

        cy.get("[data-test='cart-item']", { timeout: 10000 }).should("have.length", 0);

        cy.visit("/event/f827d813-e04a-4e84-8d69-72baef15fcd4");

        cy.get("[data-test='event-name']").then((text) => {
            const eventName = text.text();
            cy.get("[data-test='ticket-selector-container'] [data-test='ticket-selector']:first-child").as("TicketSelector").find(".d-flex > p").then((text) => {
                const ticketName = text.text();

                cy.get("@TicketSelector").find("[data-test='select-ticket']").first().find("[data-test='btn-add-ticket']").last().click();

                cy.get("[data-test='btn-buy-tickets']").first().click();

                cy.get("[data-test='nav-shopping-cart']").click();

                cy.get("[data-test='cart-item']", { timeout: 10000 }).should("have.length", 1);

                cy.get("[data-test='cart-item']").first().find("[data-test='cart-item-ticket-name']").should("have.text", ticketName);
                cy.get("[data-test='cart-item']").first().find("[data-test='cart-item-event-name']").should("have.text", eventName);
            });
        });
    });

    it('Should delete ticket in shopping cart', () => {
        cy.login(Cypress.env('testUser'), Cypress.env("testPassword"));

        cy.get("[data-test='nav-shopping-cart']").click();

        cy.get("[data-test='cart-item']", { timeout: 10000 }).should("have.length", 1);

        cy.get("[data-test='cart-item']").first().find("[data-test='btn-remove-item']").click();

        cy.get("[data-test='confirm-delete-item']").first().click();

        cy.get("[data-test='cart-item']", { timeout: 10000 }).should("have.length", 0);
    });

    it("Could buy ticket and is visable in purachse overview", () => {
        cy.login(Cypress.env('testUser'), Cypress.env("testPassword"));

        cy.get("[data-test='nav-shopping-cart']").click();

        cy.get("[data-test='cart-item']", { timeout: 10000 }).should("have.length", 0);

        cy.visit("/event/f827d813-e04a-4e84-8d69-72baef15fcd4");

        cy.get("[data-test='event-name']").then((text) => {
            const eventName = text.text();
            cy.get("[data-test='ticket-selector-container'] [data-test='ticket-selector']:first-child").as("TicketSelector").find("[data-test='selector-ticket-name']").then((text) => {
                const ticketName = text.text();

                cy.get("@TicketSelector").find("[data-test='select-ticket']").first().find("[data-test='btn-add-ticket']").last().click();

                cy.get("[data-test='btn-buy-tickets']").first().click();

                cy.get("[data-test='nav-shopping-cart']").click();

                cy.get("[data-test='cart-item']", { timeout: 10000 }).should("have.length", 1);

                cy.get("[data-test='cart-item']").first().find("[data-test='cart-item-ticket-name']").should("have.text", ticketName);
                cy.get("[data-test='cart-item']").first().find("[data-test='cart-item-event-name']").should("have.text", eventName);
            });
        });

        cy.get("#payment-button").click();

        cy.url().should("include", "/Payment-success");

        cy.get("body").contains("Aankoopnummer").then((text) => {
            const purchaseId = text.text().split("Aankoopnummer: ")[1].trim();

            cy.get("[data-test='nav-item-profile']").click();
            cy.get("#profile-ticket").click();

            cy.get("[data-test='purchase-id']").contains(purchaseId).should("exist");
        });
    });
});