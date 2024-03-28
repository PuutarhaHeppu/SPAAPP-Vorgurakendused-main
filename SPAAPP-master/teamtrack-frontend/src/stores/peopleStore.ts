import { People } from "../models/people.js";
import { ref } from "vue";
import { defineStore } from "pinia";
import useApi, { useApiRawRequest } from "../models/api.js";

export const usePeopleStore = defineStore('PeopleStore', () => {
  const apiGetPeople = useApi<People[]>('People');
  const People = ref<People[]>([]);
  let allPeople: People[] = [];

  const loadPeople = async () => {
    await apiGetPeople.request();

    if (apiGetPeople.response.value) {
      return apiGetPeople.response.value;
    }
    return [];
  };

  const load = async () => {
    allPeople = await loadPeople();
    People.value = allPeople;
  };
  const getPeopleById = (id: number) => {
    return allPeople.find((People) => People.id === id);
  };


  const addPeople = async (People: People) => {
    const apiAddPeople = useApi<People>('People', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(People),
    }); 
    
    await apiAddPeople.request();
    if (apiAddPeople.response.value) {
      load();      
    }
  };
  const updatePeople = async (People: People) => {
    const apiUpdatePeople = useApi<People>('People/' + People.id, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(People),
    });

    await apiUpdatePeople.request();
    if (apiUpdatePeople.response.value) {
      load();
    }    
  };


  const deletePeople = async (People: People) => {
    const deletePeopleRequest = useApiRawRequest(`People/${People.id}`, {
      method: 'DELETE',
    });

    const res = await deletePeopleRequest();

    if (res.status === 204) {
      let id = People.value.indexOf(People);

      if (id !== -1) {
        People.value.splice(id, 1);
      }

      id = People.value.indexOf(People);

      if (id !== -1) {
        People.value.splice(id, 1);
      }
    }
  };

  return { People, load, getPeopleById, addPeople, updatePeople, deletePeople };
});





