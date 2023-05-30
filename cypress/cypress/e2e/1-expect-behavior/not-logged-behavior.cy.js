/// <reference types="cypress" />

describe("Usuário não logado", () => {
  it("Redireciona de Contatos para a página de login", () => {
    cy.visit("localhost:5000/Contato");

    cy.get('[data-cy="login"]');
    cy.get('[data-cy="senha"]');
  });

  it("Redireciona de Usuários para a página de login", () => {
    cy.visit("localhost:5000/Usuario");

    cy.get('[data-cy="login"]');
    cy.get('[data-cy="senha"]');
  });

  it("Redireciona de AlterarSenha para a página de login", () => {
    cy.visit("localhost:5000/AlterarSenha");

    cy.get('[data-cy="login"]');
    cy.get('[data-cy="senha"]');
  });
});
