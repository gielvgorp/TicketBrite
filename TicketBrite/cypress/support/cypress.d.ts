declare namespace Cypress {
    interface Chainable {
        /**
         * Custom command to log in
         * @example cy.login('test@example.com', 'password123')
         */
        login(email: string, password: string): Chainable<void>;

        register(email: string, fullname: string, password: string): Chainable<void>;

        navigateToOrganizationPanel(): Chainable<void>;
        navigateToAdminPanel(): Chainable<void>;
        hideAllNotificaitons(): Chainable<void>;
        logout(): Chainable<void>;
    }
}