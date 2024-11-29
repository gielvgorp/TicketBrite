describe('Authentication register and login', () => {
    it('should switch between login and register', () => {
        // Bezoek de homepagina
        cy.visit('http://localhost:5173/authenticatie');

        cy.get('.text-secondary a').as('registerLink').should('exist');
        cy.get('@registerLink').should('have.text', 'Maak hier gratis een account aan!');

        cy.get('.col-8 h1').should('have.text', 'Inloggen'); // check if is on login page

        cy.get('@registerLink').click();

        cy.url().should("eq", "http://localhost:5173/authenticatie/register");

        cy.get('.text-secondary a').as('loginLink').should('exist');
        cy.get('@loginLink').should('have.text', 'Log hier in!');

        cy.get('.col-8 h1').should('have.text', 'Account aanmaken'); // check if is on regisger page

        cy.get('@loginLink').click();

        cy.url().should("eq", "http://localhost:5173/authenticatie");
    });

    it("Should show error message on not matching credentials", () => {
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#email-input").type("cypress@test.com");
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Gebruiker niet gevonden");
    });

    it("Should show error message on password empty", () => {
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#email-input").type("cypress@test.com");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Gebruiker niet gevonden");
    })

    it("Should show error message on email empty", () => {
        cy.visit('http://localhost:5173/authenticatie');

        // Vult inputs
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Gebruiker niet gevonden");
    });

    it("Should show error message on register empty full name", () => {
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#email-input").type("cypress@test.nl");
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Een of meerdere velden zijn leeg!");
    });

    it("Should show error message on register empty email empty", () => {
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#full-name-input").type("Cypress");
        cy.get("#password-input").type("cypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Een of meerdere velden zijn leeg!");
    });

    it("Should show error message on register empty password", () => {
        cy.visit('http://localhost:5173/authenticatie/register');

        // Vult inputs
        cy.get("#full-name-input").type("Cypress");
        cy.get("#email-input").type("cypress@test.nl");

        cy.get("button").click();

        cy.get(".text-danger").should("be.visible");
        cy.get(".text-danger").should("have.text", "Een of meerdere velden zijn leeg!");
    });

    it("Should show error message on email already in use", () => {
        cy.register("gielvg1@gmail.com", "Cypress", "CypressTestPass123");

        cy.get("button").click();

        cy.get(".text-danger", { timeout: 10000 }).should("be.visible");
        cy.get(".text-danger").should("have.text", "Email adres bestaat al!");
    });

    it("Should login successfull", () => {
        cy.login("cypress@e2e.com", "E2ETesting!");

        cy.get("._signInContainer_11k3u_65 a").should("have.text", "Welkom, Cypress!");
    });
});