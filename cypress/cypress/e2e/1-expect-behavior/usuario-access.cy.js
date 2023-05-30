/// <reference types="cypress" />

describe("Acesso a página de usuários", () => {
  beforeEach(() => {
    cy.visit("http://localhost:5000/Login/Sair");
    cy.visit("http://localhost:5000");

    cy.get('[data-cy="login"]').type("sus");
    cy.get('[data-cy="senha"]').type("admin");

    cy.get('[data-cy="submit"]').click();
    cy.get('[data-cy="sair"]').should("be.visible");

    cy.get('[data-cy="usuario"]').click();
  });

  it("Verificando página de Usuários", () => {
    cy.get(".display-4").contains("Listagem de usuarios");
  });

  it("Adicionar novo contato", () => {
    const nome = "Amanda";
    const email = "amanda@gmail.com";
    const login = "amanda";
    const senha = "123456";

    cy.get('[data-cy="criar"]').click();

    cy.get('[data-cy="nome"]').type(nome);
    cy.get('[data-cy="email"]').type(email);
    cy.get('[data-cy="login"]').type(login);
    cy.get('[data-cy="perfil"]').select("Padrão");
    cy.get('[data-cy="senha"]').type(senha);

    // Botão adicionar
    cy.get('[data-cy="submit"]').click();
    cy.get(".alert").contains("Usuário cadastrado com sucesso");

    cy.get("#table-usuarios tr:last").contains(nome);
    cy.get("#table-usuarios tr:last").contains(email);
    cy.get("#table-usuarios tr:last").contains(login);
  });

  it("Editar um usuário", () => {
    const nome = "Amanda";
    const novoNome = "Amanda Amaraldo";
    cy.get("#table-usuarios tr:last").contains(nome);
    // Botão Editar
    cy.get("#table-usuarios tr:last .btn-group > .btn-primary").click();

    cy.get('[data-cy="nome"]').clear().type(novoNome);

    // Botão alterar
    cy.get('[data-cy="submit"]').click();

    cy.get(".alert").contains("Usuário atualizado com sucesso");

    cy.get("#table-usuarios tr:last").contains(novoNome);
  });

  it("Apagar um contato", () => {
    const nome = "Amanda Amaraldo";
    cy.get("#table-usuarios tr:last").contains(nome);

    // Botão apagar
    cy.get("#table-usuarios tr:last .btn-group > .btn-danger").click();

    // clica em confirmar exclusão
    cy.get('[data-cy="confirmar"]').click();

    cy.get(".alert").contains("Usuário apagado com sucesso");

    cy.get("#table-usuarios tr:last").should("not.contain.text", nome);
  });

});
