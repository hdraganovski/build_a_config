import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue";
import Category from "../views/Category.vue";
import Product from "../views/Product.vue";
import LogIn from "../views/LogIn.vue";
import Register from "../views/Register.vue";
import Cart from "../views/Cart.vue";
import User from "../views/User.vue";
import Search from "../views/Search.vue";
import Mannufacturer from "../views/Mannufacturer.vue"

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
  },
  {
    path: "/category/:category",
    name: "Category",
    component: Category,
  },
  {
    path: "/search/:searchTerm",
    name: "Search",
    component: Search
  },
  {
    path: '/mannufacturer/:mannufacturer',
    name: "Mannufacturer",
    component: Mannufacturer
  },
  {
    path: "/product/:id",
    name: "Product",
    component: Product,
  },
  {
    path: "/log-in",
    name: "LogIn",
    component: LogIn,
  },
  {
    path: "/register",
    name: "Register",
    component: Register,
  },
  {
    path: "/cart",
    name: "Cart",
    component: Cart,
  },
  {
    path: "/user",
    name: "User",
    component: User,
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue"),
  },
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
