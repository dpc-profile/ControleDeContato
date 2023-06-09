/// <reference types="cypress" />

describe("Acesso a página de contatos", () => {
  beforeEach(() => {
    cy.visit("Login/Sair");

    cy.get('[data-cy="login"]').type("teste");
    cy.get('[data-cy="senha"]').type("teste");

    cy.get('[data-cy="submit"]').click();
    cy.get('[data-cy="sair"]').should("be.visible");

    cy.get('[data-cy="contato"]').click();
  });

  it("Verificando página de Contatos", () => {
    cy.get(".display-4").contains("Listagem de contatos");
  });

  it("Adicionar novo contato", () => {
    const nome = "Caio";
    const email = "caio@gmail.com";
    const celular = "11 91234-8762";

    cy.get('[data-cy="criar"]').click();

    cy.get('[data-cy="nome"]').type(nome);
    cy.get('[data-cy="email"]').type(email);
    cy.get('[data-cy="celular"]').type(celular);

    // Botão adicionar
    cy.get('[data-cy="submit"]').click();
    cy.get(".alert").contains("Contato cadastrado com sucesso");

    cy.get("#table-contatos tr:last").should("contain.text", nome);
    cy.get("#table-contatos tr:last").should("contain.text", email);
    cy.get("#table-contatos tr:last").should("contain.text", celular);
  });

  it("Editar um contato", () => {
    const nome = "Caio";
    const novoNome = "Caio Amaraldo";
    cy.get("#table-contatos tr:last").contains(nome);
    // Botão Editar
    cy.get("#table-contatos tr:last .btn-group > .btn-primary").click();

    cy.get('[data-cy="nome"]').clear().type(novoNome);

    // Botão alterar
    cy.get('[data-cy="submit"]').click();

    cy.get(".alert").contains("Contato atualizado com sucesso");

    cy.get("#table-contatos tr:last").contains(novoNome);
  });

  it("Apagar um contato", () => {
    const nome = "Caio Amaraldo";
    cy.get("#table-contatos tr:last").contains(nome);

    // Botão apagar
    cy.get("#table-contatos tr:last .btn-group > .btn-danger").click();

    // clica em confirmar exclusão
    cy.get('[data-cy="confirmar"]').click();

    cy.get(".alert").contains("Contato apagado com sucesso");

    cy.get("#table-contatos tr:last").should("not.contain.text", nome);
  });
});
