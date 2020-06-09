<template>
  <div id="app">
    <b-navbar toggleable="lg" type="dark" sticky variant="primary">
      <b-navbar-brand to="/">NavBar</b-navbar-brand>

      <b-navbar-toggle target="nav-collapse"></b-navbar-toggle>

      <b-collapse id="nav-collapse" is-nav>
        <b-navbar-nav>
          <!-- <b-nav-item href="#">Link</b-nav-item>
          <b-nav-item href="#" disabled>Disabled</b-nav-item>-->
        </b-navbar-nav>

        <!-- Right aligned nav items -->
        <b-navbar-nav class="ml-auto">
          <b-nav-form @submit="onSearch">
            <b-form-input size="sm" class="mr-sm-2" placeholder="Search" v-model="searchTerm"></b-form-input>
            <b-button size="sm" class="my-2 my-sm-0" type="submit">Search</b-button>
          </b-nav-form>

          <template v-if="loggedIn">
            <b-nav-item to="/cart">Cart</b-nav-item>
            <b-nav-item-dropdown right>
              <!-- Using 'button-content' slot -->
              <template v-slot:button-content>
                <em>{{user.fullName}}</em>
              </template>
              <b-dropdown-item to="/user" href="#">Profile</b-dropdown-item>
              <b-dropdown-item @click="logOut">Sign Out</b-dropdown-item>
            </b-nav-item-dropdown>
          </template>
          <b-nav-item to="/log-in" v-else>Log in</b-nav-item>
        </b-navbar-nav>
      </b-collapse>
    </b-navbar>
    <router-view />
  </div>
</template>

<script>
export default {
  data: function() {
    return {
      searchTerm: ""
    };
  },
  mounted() {
    this.$store.dispatch("loadCategories");
  },
  methods: {
    logOut() {
      this.$store.dispatch("logOut");
    },
    onSearch() {
      this.$router.push(`/search/${this.searchTerm}`);
    }
  },
  computed: {
    userState() {
      return this.$store.state.userState;
    },
    user() {
      return this.userState.user;
    },
    loggedIn() {
      return !!this.user;
    }
  }
};
</script>

<style lang="scss">
@import "~bootstrap";
@import "~bootstrap-vue";

html,
body {
  min-height: 100vh;
  background: #f5f5f5;
}
</style>