import Vue from "vue";
import VueRouter from "vue-router";
import HomeView from "../views/Machine/HomeView.vue";
import LoginView from "../views/User/LoginView.vue";
import MachineView from "../views/Machine/MachineView.vue";
import WorkView from "../views/Work/WorkView.vue";
import WorkEditView from "../views/Work/WorkEditView.vue";
import MachineEditView from "../views/Machine/MachineEditView.vue";
import MachineCreateView from "../views/Machine/MachineCreateView.vue";
import WorkCreateView from "../views/Work/WorkCreateView.vue";
import PasswordUpdateView from "../views/User/PasswordUpdateView.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/login",
    name: "login",
    component: LoginView
  },
  {
    path: "/",
    name: "home",
    component: HomeView,
  },
  {
    path: "/machine/:id",
    name: "machine",
    component: MachineView,
  },
  {
    path: "/machine/:machineId/work/:id",
    name: "work",
    component: WorkView,
  },
  {
    path: "/machine/work/edit/:id",
    name: "workedit",
    component: WorkEditView,
  },
  {
    path: "/machine/:id/work/create",
    name: "workcreate",
    component: WorkCreateView,
  },
  {
    path: "/machine/edit/:id",
    name: "machineedit",
    component: MachineEditView,
  },
  {
    path: "/createMachine",
    name: "machinecreate",
    component: MachineCreateView,
  },
  {
    path: "/password/update",
    name: "passwordupdate",
    component: PasswordUpdateView,
  }
];

const router = new VueRouter({
  routes,
});

router.beforeEach((to, from, next) => {
  console.log(to);
  if (to.name !== "login" && !localStorage.getItem("token")) next({ name: "login" });
  else next();
});

export default router;
