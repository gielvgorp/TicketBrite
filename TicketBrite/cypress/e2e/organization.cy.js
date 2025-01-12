/// <reference types="cypress" />

describe("Test organization role", () => {
    it("Should organization tab not show when have no permission", () => {
        cy.login(Cypress.env('testUser'), Cypress.env("testPassword"));

        cy.get("[data-test='nav-item-profile']").click();
        cy.get("#profile-organization").should("not.exist");
    });

    it('Should organization tab show when have permission', () => {
        cy.login(Cypress.env('testOrganization'), Cypress.env('testPassword'));

        cy.get("[data-test='nav-item-profile']").click();
        cy.get("#profile-organization").should("exist");
    });

    it('Should create new event', () => {
        cy.login(Cypress.env('testOrganization'), Cypress.env('testPassword'));

        cy.navigateToOrganizationPanel();

        cy.get("[data-test='btn-new-event']").click();
        cy.get(".add-event-modal").should("have.class", "show");

        cy.get("[data-test='event-name']").type("Cypress test event");
        cy.get("[data-test='event-dateTime']").type('2030-12-25');
        cy.get("[data-test='event-location']").type("Fontys Rachelsmolen");
        cy.get("[data-test='event-age']").type("18");
        cy.get("[data-test='event-category']").type("E2E Testing");
        cy.get("[data-test='event-image']").type("https://miro.medium.com/v2/resize:fit:785/1*uBf3SgcGi-I6Sml9aG10kw.png");
        cy.get("[data-test='event-description']").type("Dit is een evenement wat is gemaakt door een Cypress test!");

        cy.get("[data-test='btn-submit-new-event']").click();
    });

    it('New event should not be visable on public event overview', () => {
        cy.visit(Cypress.env('baseUrl'));

        cy.get("[data-test='event-grid']").as("eventGrid").should('exist');
        cy.get('@eventGrid').find("[data-test='grid-item']").should('have.length.greaterThan', 0);

        cy.get('@eventGrid').find("[data-test='grid-item'] [data-test='grid-item-artist']").contains("Cypress test event").should("not.exist");
    });

    it('Created event should be visable in unverified events list', () => {
        cy.login(Cypress.env('testOrganization'), Cypress.env('testPassword'));

        cy.navigateToOrganizationPanel();

        cy.get("#list-group-unverified-events").as("listGroup");
        cy.get("@listGroup").find("[data-test='list-group-item']").should("have.length", 1);

        cy.get("@listGroup").find("[data-test='list-group-item'] h5").contains("Cypress test event").should("exist");
    });


    it('Organisator should be able to edit info of event', () => {
        cy.login(Cypress.env('testOrganization'), Cypress.env('testPassword'));

        cy.navigateToOrganizationPanel();

        cy.get("[data-test='list-group-unverified-events']").as("listGroup");
        cy.get("@listGroup").find("[data-test='list-group-item']").should("have.length", 1);

        cy.get("@listGroup").find("[data-test='list-group-item'] h5").contains("Cypress test event").should("exist");

        cy.get("@listGroup").find("[data-test='list-group-item']:first-child #btn-open-dashboard").click();

        cy.url().should("contain", "/organisatie/dashboard/");

        cy.get("#event-name-input").type(" edited");

        cy.get("#btn-save-event-info").click();

        cy.navigateToOrganizationPanel();

        cy.get("#list-group-unverified-events").as("listGroup");
        cy.get("@listGroup").find("[data-test='list-group-item']").should("have.length", 1);

        cy.get("@listGroup").find("[data-test='list-group-item'] h5").contains("Cypress test event edited").should("exist");
    });
});
