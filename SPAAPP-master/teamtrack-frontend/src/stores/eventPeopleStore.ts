import { EventPeople } from "../models/EventPeople.js";
import { ref } from "vue";
import { defineStore } from "pinia";
import useApi, { useApiRawRequest } from "../models/api.js";

export const useEventPeopleStore = defineStore('EventPeopleStore', () => {
  const apiGetEventPeople = useApi<EventPeople[]>('EventPeople');
  const EventPeople = ref<EventPeople[]>([]);
  let allEventPeople: EventPeople[] = [];

  const loadEventPeople = async () => {
    await apiGetEventPeople.request();

    if (apiGetEventPeople.response.value) {
      return apiGetEventPeople.response.value;
    }
    return [];
  };

  const load = async () => {
    allEventPeople = await loadEventPeople();
    EventPeople.value = allEventPeople;
  };
  const getEventPeopleById = (id: number) => {
    return allEventPeople.find((EventPeople) => EventPeople.id === id);
  };


  const addEventPeople = async (EventPeople: EventPeople) => {
    const apiAddEventPeople = useApi<EventPeople>('EventPeople', {
      method: 'POST',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(EventPeople),
    }); 
    
    await apiAddEventPeople.request();
    if (apiAddEventPeople.response.value) {
      load();      
    }
  };
  const updateEventPeople = async (EventPeople: EventPeople) => {
    const apiUpdateEventPeople = useApi<EventPeople>('EventPeople/' + EventPeople.id, {
      method: 'PUT',
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(EventPeople),
    });

    await apiUpdateEventPeople.request();
    if (apiUpdateEventPeople.response.value) {
      load();
    }    
  };


  const deleteEventPeople = async (EventPeople: EventPeople) => {
    const deleteEventPeopleRequest = useApiRawRequest(`EventPeople/${EventPeople.id}`, {
      method: 'DELETE',
    });

    const res = await deleteEventPeopleRequest();

    if (res.status === 204) {
      let id = EventPeople.value.indexOf(EventPeople);

      if (id !== -1) {
        EventPeople.value.splice(id, 1);
      }

      id = EventPeople.value.indexOf(EventPeople);

      if (id !== -1) {
        EventPeople.value.splice(id, 1);
      }
    }
  };

  return { EventPeople, load, getEventPeopleById, addEventPeople, updateEventPeople, deleteEventPeople };
});





