/// <reference types="cypress" />

describe('Authentication register and login', () => {
    it('should switch between login and register', () => {
        cy.logout();
        // Bezoek de homepagina
        cy.visit('http://localhost:5173/authenticatie');

        cy.get('.text-secondary a').as('registerLink').should('exist');
        cy.get('@registerLink').should('have.text', 'Maak hier gratis een account aan!');

        cy.get('.col-8 h1').should('have.text', 'Inloggen'); // check if is on login page

        cy.get('@registerLink').click();

        cy.url().should("eq", "http://localhost:5173/authenticatie/register");

        cy.get('.text-secondary a').as('loginLink').should('exist');
        cy.get('@loginLink').should('have.text', 'Log hier in!');

        cy.get('.col-8 h1').should('have.text', 'Account aanmaken');

        cy.get('@loginLink').click();

        cy.url().should("eq", "http://localhost:5173/authenticatie");
    });

    it("Should show error message on not matching credentials", () => {
        cy.logout();
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#email-input").type("cypress@test.com");
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get("[data-test='validation-message']").should("be.visible");
        cy.get("[data-test='validation-message']").should("have.text", "Email of wachtwoord is onjuist!");
    });

    it("Should show error message on password empty", () => {
        cy.logout();
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#email-input").type("cypress@test.com");

        cy.get("button").click();

        cy.get("[data-test='validation-message']").should("be.visible");
        cy.get("[data-test='validation-message']").should("have.text", `Veld "Wachtwoord" is verplicht maar is leeg!`);
    })

    it("Should show error message on email empty", () => {
        cy.logout();
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get("[data-test='validation-message']").should("be.visible");
        cy.get("[data-test='validation-message']").should("have.text", `Veld "Email adres" is verplicht maar is leeg!`);
    });

    it("Should show error message on register empty full name", () => {
        cy.logout();
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#email-input").type("cypress@test.nl");
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get("[data-test='validation-message']").should("be.visible");
        cy.get("[data-test='validation-message']").should("have.text", "Je volledige naam mag niet leeg zijn!");
    });

    it("Should show error message on register empty email empty", () => {
        cy.logout();
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#full-name-input").type("Cypress");
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get("[data-test='validation-message']").should("be.visible");
        cy.get("[data-test='validation-message']").should("have.text", "Email adres mag niet leeg zijn!");
    });

    it("Should show error message on register empty password", () => {
        cy.logout();
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#full-name-input").type("Cypress");
        cy.get("#email-input").type("cypress@test.nl");

        cy.get("button").click();

        cy.get("[data-test='validation-message']").should("be.visible");
        cy.get("[data-test='validation-message']").should("have.text", "Wachtwoord mag niet leeg zijn!");
    });

    it("Should show error message on email already in use", () => {
        cy.logout();
        cy.register("Cypress@e2e.com", "Cypress", "CypressTestPass123");

        cy.get("button").click();

        cy.get("[data-test='validation-message']", { timeout: 10000 }).should("be.visible");
        cy.get("[data-test='validation-message']").should("have.text", "Het opgegeven e-mailadres bestaat al!");
    });

    it("Should login successfull", () => {
        cy.logout();
        cy.login("cypress@e2e.com", "E2ETesting!");

        cy.get(".sign-in-container a").should("have.text", "Welkom, Cypress!");
    });
});